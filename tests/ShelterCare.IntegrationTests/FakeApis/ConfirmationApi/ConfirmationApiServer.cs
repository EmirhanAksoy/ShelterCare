using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ShelterCare.IntegrationTests.FakeApis;

public class ConfirmationApiServer : IDisposable
{

    private WireMockServer _wireMockServer;
    public Uri Uri => new Uri(_wireMockServer.Url);
    public void SetupValidOwnerConfirmationEndpointV1()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/national-id/confirm/{ValidAnimalOwnerMapV1.OwnerNationalId}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"message\": \"\"\r\n}"));
    }

    public void SetupValidOwnerConfirmationEndpointV2()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/national-id/confirm/{ValidAnimalOwnerMapV2.OwnerNationalId}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"message\": \"\"\r\n}"));
    }

    public void SetupValidAnimalConfirmationEndpointV1_LOKI()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/animal/id/confirm/{ValidAnimalOwnerMapV1.LOKI_IDENTIFIER}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"data\": {\r\n    \"name\": \"LOKI\",\r\n    \"id\": \"1234-LOKI\",\r\n    \"ownerId\": \"1234\"\r\n  }\r\n}"));
    }

    public void SetupValidAnimalConfirmationEndpointV1_LOKUM()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/animal/id/confirm/{ValidAnimalOwnerMapV1.LOKUM_IDENTIFIER}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"data\": {\r\n    \"name\": \"LOKUM\",\r\n    \"id\": \"1234-LOKUM\",\r\n    \"ownerId\": \"1234\"\r\n  }\r\n}"));
    }

    public void SetupValidAnimalConfirmationEndpointV2_BOB()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/animal/id/confirm/{ValidAnimalOwnerMapV2.BOB_IDENTIFIER}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"data\": {\r\n    \"name\": \"BOB\",\r\n    \"id\": \"2345-BOB\",\r\n    \"ownerId\": \"2345\"\r\n  }\r\n}"));
    }

    public void SetupValidAnimalOwnerMatchConfirmationEndpoint_V1_LOKI()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/animal/owner/confirm/{ValidAnimalOwnerMapV1.OwnerNationalId}/{ValidAnimalOwnerMapV1.LOKI_IDENTIFIER}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"message\": \"Matching\"\r\n}"));
    }

    public void SetupValidAnimalOwnerMatchConfirmationEndpoint_V1_LOKUM()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/animal/owner/confirm/{ValidAnimalOwnerMapV1.OwnerNationalId}/{ValidAnimalOwnerMapV1.LOKUM_IDENTIFIER}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"message\": \"Matching\"\r\n}"));
    }

    public void SetupValidAnimalOwnerMatchConfirmationEndpoint_V2_BOB()
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/animal/owner/confirm/{ValidAnimalOwnerMapV2.OwnerNationalId}/{ValidAnimalOwnerMapV2.BOB_IDENTIFIER}")
            .UsingGet())
            .RespondWith(
                Response.Create().WithBody("{\r\n  \"success\": true,\r\n  \"message\": \"Matching\"\r\n}"));
    }

    public void Start()
    {
        _wireMockServer = WireMockServer.Start();
        SetupValidOwnerConfirmationEndpointV1();
        SetupValidOwnerConfirmationEndpointV2();
        SetupValidAnimalConfirmationEndpointV1_LOKI();
        SetupValidAnimalConfirmationEndpointV1_LOKUM();
        SetupValidAnimalConfirmationEndpointV2_BOB();
        SetupValidAnimalOwnerMatchConfirmationEndpoint_V1_LOKI();
        SetupValidAnimalOwnerMatchConfirmationEndpoint_V1_LOKUM();
        SetupValidAnimalOwnerMatchConfirmationEndpoint_V2_BOB();
    }

    public void Dispose()
    {
        _wireMockServer.Stop();
        _wireMockServer.Dispose();
    }
}
