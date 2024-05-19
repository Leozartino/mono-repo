import IncidentType from "../enums/incident-type.enum";
import SeverityType from "../enums/severity.enum";

export interface IncidentCreateRequest {
    title: string;
    description: string;
    severityType: SeverityType;
    incidentType: IncidentType;
    reportedDate: Date
    userId: string;
}