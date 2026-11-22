# Production configuration

The API now keeps production secrets out of `appsettings.json`. Configure them through environment variables, your deployment platform secret store, Azure Key Vault, AWS Secrets Manager, Kubernetes Secrets, or an equivalent provider.

Required production settings:

- `ConnectionStrings__DefaultConnection`
- `Jwt__Key` (minimum 32 characters; use a strong random secret)
- `Jwt__Issuer`
- `Jwt__Audience`
- `Cors__AllowedOrigins__0` (and additional indexed origins as required)

Example environment variable names:

```text
ConnectionStrings__DefaultConnection=<production SQL Server connection string>
Jwt__Key=<strong random secret>
Jwt__Issuer=TaxCalculation.API
Jwt__Audience=TaxCalculation.Client
Cors__AllowedOrigins__0=https://your-frontend.example.com
```

## Production behavior

- JWT authentication is required for role, privilege, user, tax and employee endpoints.
- `/api/auth/login` is anonymous.
- `/health` is available for load balancer/container health checks.
- Swagger UI is exposed only in Development.
- HTTPS redirection and forwarded proxy headers are enabled.
- SQL Server transient retries and a 30-second command timeout are configured.
- Unhandled exceptions are logged server-side and return a generic error with a trace ID instead of leaking exception details.

## Recommended deployment follow-ups

Use a real centralized logging sink (Application Insights, Serilog + Seq/ELK, CloudWatch, etc.), restrict forwarded proxy networks/hosts at infrastructure level, rotate JWT secrets, use least-privilege DB credentials, and run database migrations as a controlled deployment step rather than automatically on every API startup.
