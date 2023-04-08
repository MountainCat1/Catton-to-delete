﻿using Account.Domain.Repositories;
using MediatR;

namespace Account.Application.Features.GoogleAuthentication.AuthViaGoogle;

public class AuthiViaGoogleRequestHandler : IRequestHandler<AuthiViaGoogleRequest, string>
{
    private IAccountRepository _accountRepository;
    private IGoogleAuthProviderService _authProviderService;
    
    public AuthiViaGoogleRequestHandler(IAccountRepository accountRepository, IGoogleAuthProviderService authProviderService)
    {
        _accountRepository = accountRepository;
        _authProviderService = authProviderService;
    }

    public async Task<string> Handle(AuthiViaGoogleRequest request, CancellationToken cancellationToken)
    {
        var payload = await _authProviderService.ValidateGoogleJwtAsync(request.GoogleAuthToken);
        var account = await _accountRepository.GetOneAsync(payload.JwtId);
        
        // TODO: payload.JwtId idk if is good for this
        Console.WriteLine(account.Email);
        
        // TODO: get an account via goole id, create our JWT and return
        throw new NotImplementedException();
    }
}