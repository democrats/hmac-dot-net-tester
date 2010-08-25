# HMAC .NET Tester

Simple Windows (.NET 4) app for testing HTTP HMAC authentication. Since .NET can't set the HTTP Date header, you have to use a custom header and workaround that on the server side.

Since this can make it very annoying to debug your HMAC canonical string, it's much easier to have a simple client to isolate problems.