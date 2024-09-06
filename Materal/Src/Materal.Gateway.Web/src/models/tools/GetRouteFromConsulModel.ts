export default interface GetRouteFromConsulModel {
    Name: string;
    Tag: string;
    DangerousAcceptAnyServerCertificateValidator: boolean;
    Mode: number;
    DownstreamScheme: string;
    HttpVersion: string;
    UpstreamPathTemplate: string;
    DownstreamPathTemplate: string;
}