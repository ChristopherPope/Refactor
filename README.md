# Example of my ability to refactor a poorly written C# REST API into a properly written API using SOLID principles.

This solution contains a poorly written project named **BadProductsApi** which I created with this ChatGPT request:
> Create a .Net 10 REST API project which is a poorly written C# REST API for managing products which does not follow SOLID. 
>The API should use EF against these tables: Product, Company
>The API should use controllers`

The rest of the solution contains these projects:

 - **GoodProductsApi** - The REST API controllers.
 - **GoodProductsApi.BusinessLogic** - Business logic code.
 - **GoodProductsApi.DataAccess** - DB access code.