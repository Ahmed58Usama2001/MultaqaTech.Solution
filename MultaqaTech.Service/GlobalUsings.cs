global using MultaqaTech.Core.Entities;
global using MultaqaTech.Core.Entities.Enums;
global using MultaqaTech.Core.Specifications;
global using MultaqaTech.Core.Entities.Identity;
global using MultaqaTech.Core.Services.Contract;
global using MultaqaTech.Core.Repositories.Contract;
global using MultaqaTech.Core.Entities.Identity.Enums;
global using MultaqaTech.Core.Entities.Identity.Gmail;
global using MultaqaTech.Repository.Data.Configurations;
global using MultaqaTech.Core.Entities.SettingsEntities;
global using MultaqaTech.Core.Entities.Identity.Facebook;
global using MultaqaTech.Core.Entities.ZoomDomainEntites;
global using MultaqaTech.Core.Entities.EventDomainEntities;
global using MultaqaTech.Core.Specifications.BlogPost_Specs;
global using MultaqaTech.Core.Entities.CourseDomainEntities;
global using MultaqaTech.Core.Entities.BlogPostDomainEntities;
global using MultaqaTech.Core.Services.Contract.EventContracts;
global using MultaqaTech.Core.Services.Contract.BlogPostContracts;
global using MultaqaTech.Core.Services.Contract.AuthDomainContracts;
global using MultaqaTech.Core.Services.Contract.ZoomMeetingContracts;
global using MultaqaTech.Core.Specifications.ZoomMeetingEntitiesSpecs;
global using MultaqaTech.Core.Services.Contract.AccountModuleContracts;
global using MultaqaTech.Core.Services.Contract.CourseDomainContracts;
global using MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;
global using MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;
global using MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.Configuration;

global using System.Text;
global using System.Security.Claims;
global using System.Text.RegularExpressions;
global using System.IdentityModel.Tokens.Jwt;

global using Serilog;
global using MimeKit;
global using Newtonsoft.Json;
global using MailKit.Security;
global using MailKit.Net.Smtp;
global using StackExchange.Redis;
global using static Google.Apis.Auth.GoogleJsonWebSignature;

