using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AthenaService.CollectorCommunication.Message;
using AthenaService.CollectorCommunication.WebSocketHandler;
using AthenaService.Common.CommandText;
using AthenaService.Domain.Base;
using AthenaService.Domain.Settings.Entities;
using AthenaService.Interfaces;
using AthenaService.Persistence;

using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IPersistenceService _persistenceService;
        private readonly IWebSocketMessageHandler _webSocketMessageHandler;

        public IntegrationService(IPersistenceService persistenceService, IWebSocketMessageHandler webSocketMessageHandler)
        {
            _persistenceService = persistenceService;
            _webSocketMessageHandler = webSocketMessageHandler;
        }

        public async Task<Integration> GetByIdAsync(Guid id) =>
            await _persistenceService.QuerySingleOrDefaultAsync<Integration>(CommandIntegrationText.GetById, new { id });

        public async Task<string> GenerateIntegrationTokenAsync(Guid integrationId, int tenantId)
        {
            var integration = await GetByIdAsync(integrationId);
            if (integration == null)
            {
                // throw exception
            }

            // update tokenId in Integration
            var secretKey = Guid.NewGuid();
            var tokenId = Guid.NewGuid();
            var token = GenerateJwtToken(secretKey.ToString(), new TimeSpan(36500), "athena", "athena", integrationId, tenantId, tokenId);
            integration.TokenId = tokenId;
            await _persistenceService.UpdateAsync(integration);

            return token;
        }

        private static string GenerateJwtToken(string secretKey, TimeSpan? lifetime, string iss, string aud, Guid integrationId, int tenantId, Guid tokenId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var claims = new List<Claim>
            {
                new Claim(CommonConstants.Claims.Id, integrationId.ToString(), ClaimValueTypes.String, integrationId.ToString()),
                new Claim(CommonConstants.Claims.TenantId, tenantId.ToString(), ClaimValueTypes.String, integrationId.ToString()),
                new Claim(CommonConstants.Claims.Jti, tokenId.ToString(), ClaimValueTypes.String, integrationId.ToString())
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new(
                    iss,
                    aud,
                    claims,
                    DateTime.UtcNow,
                    lifetime.HasValue ? DateTime.UtcNow + lifetime : DateTime.MaxValue,
                    signingCredentials);

            return tokenHandler.WriteToken(token);
        }

        public async Task<IList<string>> GetIntegrationNameAsync()
        {
            var listIntegrationNames = await _persistenceService.QueryAsync<string>(CommandIntegrationText.GetAllIntegrationName);
            return listIntegrationNames.ToList();
        }

        public async Task CheckConnectionAsync(Guid integrationId, int tenantId)
        {
            var integration = await GetByIdAsync(integrationId);
            if (integration?.State == IntegrationState.Disconnected)
            {
                var message = new SocketMessage
                {
                    MessageTypeId = (int)MessageType.Integration,
                    ActionTypeId = (int)IntegrationActionType.CheckConnection,
                    ItemId = integration.IntegrationId,
                    TokenId = integration.TokenId,
                    TenantId = tenantId,
                };
                await _webSocketMessageHandler.BroadcastMessage(JsonSerializer.Serialize(message));
            }
        }

        public async Task<int> UpdateStateByTokenIdAsync(Guid tokenId, IntegrationState state)
        {
            var param = new { tokenId, state, stateUpdatedAt = DateTime.UtcNow };
            return await _persistenceService.ExecuteAsync(CommandIntegrationText.UpdateIntegrationStateByTokenId, param);
        }
    }
}
