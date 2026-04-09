using DotNetEnv;
using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Interfaces;
using GerenciamentoPatrimonio.Repositories;
using GerenciamentoPatrimonio.Applications.Autenticacao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// carregando o .env
Env.Load();

// pegando a connection string 
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Conexao com o Banco 
builder.Services.AddDbContext<GerenciamentoSenaiDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// AREAS
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();

// LOCALIZACOES 
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();

// USUARIOS 
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

// CIDADE 
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();

// BAIRRO
builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<BairroService>();

// ENDERECO
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<EnderecoService>();

// LOG PATRIMONIO 
builder.Services.AddScoped<ILogPatrimonioRepository, LogPatrimonioRepository>();
builder.Services.AddScoped<LogPatrimonioService>();

// STATUS PATRIMONIO
builder.Services.AddScoped<IStatusPatrimonioRepository, StatusPatrimonioRepository>();
builder.Services.AddScoped<StatusPatrimonioService>();

// SOLICITACAO TRANSFERENCIA 
builder.Services.AddScoped<ISolicitacaoTransferenciaRepository, SolicitacaoTransferenciaRepository>();
builder.Services.AddScoped<SolicitacaoTransferenciaService>();

// JWT 
builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

// Configura o sistema de autenticação da aplicação.
// Aqui estamos dizendo que o tipo de autenticação padrão será JWT Bearer.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Adiciona o suporte para autenticação usando JWT.
    .AddJwtBearer(options =>
    {
        // Lê a chave secreta definida no appsettings.json.
        var chave = Environment.GetEnvironmentVariable("JWT_KEY");
        //var chave = builder.Configuration["Jwt:Key"]!;

        // Quem emitiu o token.
        var issuer = builder.Configuration["Jwt:Issuer"]!;

        // Para quem o token foi criado.
        var audience = builder.Configuration["Jwt:Audience"]!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Verifica se o emissor do token é válido.
            ValidateIssuer = true,

            // Verifica se o destinatário do token é válido.
            ValidateAudience = true,

            // Verifica se o token ainda está válido.
            ValidateLifetime = true,

            // Verifica se a assinatura do token é válida.
            ValidateIssuerSigningKey = true,

            // Define qual emissor é considerado válido.
            ValidIssuer = issuer,

            // Define qual audience é considerado válido.
            ValidAudience = audience,

            // Define qual chave será usada para validar a assinatura do token.
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            ),

            // o token geralmente tem 5 minutos de tolerancia, aqui colocamos para remover essa tolerancia
            // remove tolerância extra no vencimento do token
            ClockSkew = TimeSpan.Zero
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
