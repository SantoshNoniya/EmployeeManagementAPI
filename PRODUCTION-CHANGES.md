# Production hardening changes

- Added centralized `GlobalExceptionMiddleware` with server-side logging and trace IDs.
- Removed repetitive controller-level `try/catch` blocks and preserved service HTTP status codes.
- Added `[Authorize]` to protected API controllers and `[AllowAnonymous]` only to login.
- Added JWT Bearer support to Swagger.
- Moved production connection string/JWT secret out of base `appsettings.json`.
- Made CORS origins configuration-driven.
- Added HTTPS redirection and forwarded-header support for reverse proxy/container hosting.
- Added SQL Server transient retry handling and command timeout.
- Added `/health` endpoint.
- Removed sample WeatherForecast API.
- Normalized REST-style routes for auth, roles, privileges, users, tax, and employees.
- Added login DTO validation.
- Fixed role deletion null dereference.
- Fixed user update so original CreatedDate/CreatedBy are not overwritten.
- User update now only replaces the password when a new password is supplied.
- Replaced local-time audit writes in RoleService with UTC.
- Removed role-service exception detail leakage from API responses and added structured logging.
- Added production deployment/configuration guidance in `TaxCalculation.API/PRODUCTION-README.md`.
