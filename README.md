# Auth0 Playground

Test project to show how auth0 can be used for authorization and authentification:

- Login
- Authentification with bearer token
- Authorization with user's scopes

### Quickstart guide to Auth0 account setup

#### ManagementApi (Server/Machine-To-Machine)

1. Create Test App (Machine-To-Machine)
![image](https://user-images.githubusercontent.com/17797666/210096302-d6a59c42-55c1-455d-85e4-70ead59ac77a.png)

2. Create test user

3. Generate client token:
POST `https://{domain}/oauth/token`
```
{
    "client_id":"***",
    "client_secret":"***",
    "audience":"https://{domain}/api/v2/",
    "grant_type":"client_credentials"
}
```

Audience can be found in Api (API Identifier) that is used by the application:
![image](https://user-images.githubusercontent.com/17797666/210096620-92d269bc-cc90-42cd-8034-40b6537b6af7.png)

![image](https://user-images.githubusercontent.com/17797666/210096662-c111226a-faa0-4cf9-a910-2812ab23a56a.png)

Client id and secret can be found on Settings tab of the application
![image](https://user-images.githubusercontent.com/17797666/210096763-fc7e1fdf-0090-4295-9df0-4edd5323bff2.png)

4. Get test user data with generated token
GET `https://{domain}/api/v2/users/{user_id}`


#### Authentication Api (Client)

1. Create Client Api and set audience as Identifier:
![image](https://user-images.githubusercontent.com/17797666/210096908-577c4542-44a0-4fbd-8ce3-2bebb069478a.png)

2. Set default audience in [Tenant Settings](https://auth0.com/docs/get-started/tenant-settings):
![image](https://user-images.githubusercontent.com/17797666/210096935-4d022b4c-7d74-458b-af0f-5f630aba6484.png)

### Password

1. Configure client to allow password grant-type
1.1 Settings -> Advanced Settings
![image](https://user-images.githubusercontent.com/17797666/210096980-dc55d044-210d-4e2b-a43d-df503314b487.png)

1.2 Dashboard -> Settings -> Api Authorization Settings set Username-Password-Authentication in Default Directory
![image](https://user-images.githubusercontent.com/17797666/210097011-d448f31e-c3bb-4b68-bf75-951b109924a8.png)

2. Create token for a user that was created before:

POST `https://{domain}/oauth/token`
```
{
    "client_id":"***",
    "grant_type":"password",
    "username": "***",
    "password": "***"
}
```

Playground: Post `/login`

To make this endpoint public (usable by third parties who know only client_id) set application setting "Token Endpoint Authentication Method" to None
![image](https://user-images.githubusercontent.com/17797666/210097083-0e79698a-1528-4520-a756-ef47175241a8.png)

Note: Access to application with this settings can not authenticated with client credentials.

3. Check token by getting info

Playground: Get `/info`

## Permissions

1. Add scopes to Client Api
![image](https://user-images.githubusercontent.com/17797666/210097168-11fb5260-c677-4cb7-a538-0939ec7f7b63.png)

2. Set permissions to user:
![image](https://user-images.githubusercontent.com/17797666/210097248-a5091031-930b-402d-a83b-a488dfe9c3f3.png)

3. Create token and see scopes in this token:
![image](https://user-images.githubusercontent.com/17797666/210097389-ff4c0e9b-9985-44a6-9d2d-5d9bf574d998.png)
