using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardStorageService.Services.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        #region Services

        private readonly IServiceScopeFactory _serviceScopeFactory;

        #endregion

        public const string SecretKey = "kYp4s6v9/B&E(nw3n2@";

        private readonly Dictionary<string, SessionInfo> _session = new Dictionary<string, SessionInfo>();

        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            CardStorageServiceDbContext context = scope.ServiceProvider.GetService<CardStorageServiceDbContext>();

            Account account = !string.IsNullOrWhiteSpace(authenticationRequest.Login) ?
                FindAccountByLogin(context, authenticationRequest.Login) : null;

            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }
            if (!PasswordUtils.VerifyPassowrd(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse { Status = AuthenticationStatus.InvalidPassword };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreate= DateTime.UtcNow,
                TimeLastRequest = DateTime.UtcNow,
                IsClosed= false,
            };

            context.Accounts.Add(account);

            context.SaveChanges();

            SessionInfo sessionInfo = GetSessionInfo(account,session);

            //if (!_session.ContainsK ey(sessionInfo.SessionToken))
            //    _session.Add(sessionInfo.SessionToken, sessionInfo);
            //else
            //    _session[sessionInfo.SessionToken] = sessionInfo;
            lock(_session)
            {
                _session[sessionInfo.SessionToken] = sessionInfo;
            }

            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                SessionInfo = sessionInfo
            };
        }

        public SessionInfo GetSessionInfo(string sessionToken)
        {
            SessionInfo sessionInfo;
            
            lock(_session)
            { 
                _session.TryGetValue(sessionToken, out sessionInfo); 
            }

            if (sessionInfo == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                CardStorageServiceDbContext context = scope.ServiceProvider.GetService<CardStorageServiceDbContext>();

                AccountSession session = context
                    .AccountSessions.FirstOrDefault(item => item.SessionToken == sessionToken);

                if (session == null)
                    return null;

                Account account = context.Accounts.FirstOrDefault(item => item.AccountId == session.AccountId);

                sessionInfo = GetSessionInfo(account,session);

                if (sessionInfo != null)
                {
                    lock(_session)
                    {
                        _session[sessionToken] = sessionInfo;
                    }
                }

            }

            return sessionInfo;

        }


        private SessionInfo GetSessionInfo (Account account, AccountSession accountSession)
        {
            return new SessionInfo
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    Email = account.Email,
                    FirstName = account.Firstname,
                    LastName = account.Lastname,
                    SecondName= account.Secondname,
                    Locked= account.Locked,
                }
            };
        }


        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                        new Claim(ClaimTypes.Email, account.Email),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private Account FindAccountByLogin(CardStorageServiceDbContext context, string login) 
        {
            return context.Accounts.FirstOrDefault(account => account.Email == login);
        }


    }
}
