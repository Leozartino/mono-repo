import IncidentType from "../enums/incident-type.enum";
import SeverityType from "../enums/severity.enum";

export default interface IncidentResponse {
    id: string;
    title: string;
    description: string;
    reportedDate: Date;
    severityType: SeverityType;
    incidentType: IncidentType;
    createdAt: Date;
    updatedAt: Date | null;
}