import { Injectable } from '@angular/core';
import { IncidentCreateRequest } from '../shared/models/incident-create-request';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environment';
import { MatSnackBar } from '@angular/material/snack-bar';
import IncidentResponse from '../shared/models/incident-response';
import IncidentUpdateRequest from '../shared/models/incident-update-request';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {

  public baseUrl = environment.apiUrl;
  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }


  public showMessage(message: string): void {
    this.snackBar.open(message, 'X', {
      duration: 3500,
      horizontalPosition: 'right',
      verticalPosition: 'top'
    })
  }
  public getIncidentById(id:string): Observable<IncidentResponse> {
    return this.http.get<IncidentResponse>(`${this.baseUrl}/incident/${id}`);
  }


  public getIncidentsByUserId(id:string): Observable<IncidentResponse[]> {
    return this.http.get<IncidentResponse[]>(`${this.baseUrl}/incident/byUser/${id}`);
    
  }

  public getAllIncidents(): Observable<IncidentResponse[]> {
    return this.http.get<IncidentResponse[]>(`${this.baseUrl}/incident`);
  }

  public createIncident(incidentCreateRequest: IncidentCreateRequest): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/incident`, incidentCreateRequest);
  }

  public updateIncident(id: string, incidentUpdateRequest: IncidentUpdateRequest): Observable<IncidentResponse> {
    return this.http.put<IncidentResponse>(`${this.baseUrl}/incident/${id}`, incidentUpdateRequest);
  }

  public deleteIncident(id:string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/incident/${id}`);
  }
}
