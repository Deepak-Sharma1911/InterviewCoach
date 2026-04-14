using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application.Wrappers
{
    public record TokenRequest(
     string Username,
     string Password
 );

    public record TokenResponse(
        string AccessToken,
        string RefreshToken,
        int ExpiresIn,
        string TokenType
    );

    public record RefreshTokenRequest(
        string RefreshToken
    );
}
