// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catalog"){Scopes=new List<string> { "catalog_fullpermission"}},
                new ApiResource("resource_photo_stock"){Scopes=new List<string> { "photo_stock_fullpermission"}},
                new ApiResource("resource_basket"){Scopes=new List<string> { "basket_fullpermission"}},
                new ApiResource("resource_discount"){Scopes=new List<string> { "discount_fullpermission"}},
                new ApiResource("resource_order"){Scopes=new List<string> { "order_fullpermission"}},
                new ApiResource("resource_payment"){Scopes=new List<string> { "payment_fullpermission"}},
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };


        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name="roles",DisplayName="Roller",Description="Kullanıcı rolleri",UserClaims=new []{"role" } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog Api için full yetki"),
                new ApiScope("photo_stock_fullpermission","Photo Stock Api için full yetki"),
                new ApiScope("basket_fullpermission","Basket Api için full yetki"),
                new ApiScope("discount_fullpermission","Discount Api için full yetki"),
                new ApiScope("order_fullpermission","Order Api için full yetki"),
                new ApiScope("payment_fullpermission","Payment Api için full yetki"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "AspNet Core Mvc",
                    ClientId ="WebMvcClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes=new List<string> { "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientName = "AspNet Core Mvc",
                    ClientId ="WebMvcClientForUser",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes=new List<string>
                    {
                        "basket_fullpermission",
                        "discount_fullpermission",
                        "order_fullpermission",
                        "payment_fullpermission",
                        IdentityServerConstants.StandardScopes.Email ,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles"
                    },
                    AccessTokenLifetime=3600,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage=TokenUsage.ReUse
                }
            };
    }
}