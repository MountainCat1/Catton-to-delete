﻿using Account.Application.Services.Abstractions;
using Account.Application.Settings;
using Account.Domain.Entities;
using Google.Apis.Auth;

namespace Account.Application.Features.GoogleAuthentication;

public interface IGoogleAuthProviderService : IAuthProviderService<GoogleAccountEntity, GoogleAuthenticationData>
{
    /// <summary>
    /// Validates google JWT and its return payload
    /// </summary>
    /// <param name="jwt"></param>
    /// <returns></returns>
    Task<GoogleJsonWebSignature.Payload> ValidateGoogleJwtAsync(string jwt);
}

public class GoogleAuthProviderService : IGoogleAuthProviderService
{
    private readonly AuthenticationConfig _authenticationConfig;
    private readonly ILogger<IGoogleAuthProviderService> _logger;
    
    public GoogleAuthProviderService(
        AuthenticationConfig authenticationConfig, 
        ILogger<IGoogleAuthProviderService> logger)
    {
        _authenticationConfig = authenticationConfig;
        _logger = logger;
    }

    /// <summary>
    /// Validates google JWT and its return payload
    /// </summary>
    /// <param name="jwt"></param>
    /// <returns></returns>
    public async Task<GoogleJsonWebSignature.Payload> ValidateGoogleJwtAsync(string jwt)
    {
        if (jwt.Contains(' '))
            jwt = jwt.Split(' ').Last();

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                jwt, 
                new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience =  new[] { _authenticationConfig.Google.ClientId }
                }
            );
            return payload;
        }
        catch (Exception e)
        {
            throw;
        }
    }
    
}