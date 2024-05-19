import IncidentType from "../enums/incident-type.enum"
import Severity from "../enums/severity.enum"

export default interface IncidentUpdateRequest {
    title?: string
    description?: string
    severityType?: Severity
    incidentType?: IncidentType
    reportedDate?: Date
    updatedBy: string
}