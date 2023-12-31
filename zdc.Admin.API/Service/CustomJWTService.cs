﻿using Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Dto.User;
using Model.Entitys;
using Model.Other;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomJWTService : ICustomJWTService
    {
        private readonly JWTTokenOptions _JWTTokenOptions;
        public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        public async Task<string> GetToken(User user)
        {
            if(user == null)
            {
                return null;
            }
            var result = await Task.Run(() =>
            {
                // 有效载荷,要避免敏感信息
                var claims = new[]
{
                    new Claim("Id",user.Id),
                    new Claim("Uid", user.Uid),
                    new Claim("Name", user.Name),
                    new Claim("Type", user.Type),
                };
                //需要加密：需要加密key:
                //Nuget引入：Microsoft.IdentityModel.Tokens
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Nuget引入：System.IdentityModel.Tokens.Jwt
                JwtSecurityToken token = new JwtSecurityToken(
                 issuer: _JWTTokenOptions.Issuer,
                 audience: _JWTTokenOptions.Audience,
                 claims: claims,    // 参数
                 expires: DateTime.Now.AddMinutes(10),//10分钟有效期 
                 notBefore: null,
                 signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
            return result;
        }
    }
}
