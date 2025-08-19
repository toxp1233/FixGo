fixgo-api!
Overview
fixgo-api is a backend API service for managing service requests. It handles user requests, phone verification, and media uploads, providing secure access and smooth token-based authentication.

Tech Stack
C# with .NET 8

Azure Hosting & Azure Database

Phone verification via Twilio

Cloudinary for storing videos and photos

Deployment
The API is ready to be deployed to azure

Authentication & Token Management
Access tokens are valid for 15 minutes.

Refresh tokens are valid for 7 days.

To maintain user sessions, the front-end should request a new access token using the refresh token and user ID before expiration.

How to Use
Use the Swagger interface to explore all available endpoints once its deployed.

Ensure the front-end passes valid tokens for authorized requests.

Media uploads support photos and videos via Cloudinary.

Notes
All necessary environment configurations are set and working in the deployed environment.
Phone verification is handled via Twilio integration.

this project is the first i ever created and not my last!
