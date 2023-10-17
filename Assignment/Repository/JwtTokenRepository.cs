using Assignment.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Assignment.Repository
{
    public class JwtTokenRepository:IJwtToken
    {
        //public IActionResult CheckTokenExpiration()
        //{
            //// Get the user's claims, including the expiration claim
            //var claims = User.Claims;

            //// Find the expiration claim
            //var expirationClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration);

            //if (expirationClaim != null)
            //{
            //    var expirationValue = Convert.ToInt64(expirationClaim.Value);
            //    var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationValue).UtcDateTime;

            //    if (expirationDateTime < DateTime.UtcNow)
            //    {
            //        // The token has expired
            //        return BadRequest("Token has expired.");
            //    }

            //    // The token is valid
            //    return Ok("Token is valid.");
            //}

            //// If there is no expiration claim, it's best to deny access or consider the token invalid
            //return BadRequest("Token is invalid.");
       // }
    }
}
