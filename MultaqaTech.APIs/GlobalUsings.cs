global using MultaqaTech.Service;
global using MultaqaTech.APIs.Dtos;
global using MultaqaTech.Repository;
global using MultaqaTech.APIs.Errors;
global using MultaqaTech.APIs.Helpers;
global using MultaqaTech.Core.Entities;
global using MultaqaTech.APIs.Extensions;
global using MultaqaTech.Repository.Data;
global using MultaqaTech.APIs.MiddleWares;
global using MultaqaTech.Repository.Identity;
global using MultaqaTech.Core.Services.Contract;
global using MultaqaTech.Core.Entities.Identity;
global using MultaqaTech.Core.Repositories.Contract;
global using MultaqaTech.Core.Entities.CourseDomainEntities;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using System.Net;
global using System.Text;
global using System.Linq;
global using System.Net.Mail;
global using System.Text.Json;
global using System.Security.Claims;
global using System.ComponentModel.DataAnnotations;

global using AutoMapper;