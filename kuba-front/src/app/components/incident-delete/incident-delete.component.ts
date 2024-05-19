import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import IncidentResponse from '../../shared/models/incident-response';
import IncidentType from '../../shared/enums/incident-type.enum';
import SeverityType from '../../shared/enums/severity.enum';
import { IncidentService } from '../../services/incident.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'kuba-app-incident-delete',
  standalone: true,
  providers: [
    provideNativeDateAdapter()
  ],
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, 
     CommonModule, MatDatepickerModule,MatButtonModule],
  templateUrl: './incident-delete.component.html',
  styleUrl: './incident-delete.component.css'
})
export class IncidentDeleteComponent implements OnInit{

  public userId: string = localStorage.getItem('id')!;
  public idCurrentIncident: string = "";
  public incident: IncidentResponse = {} as IncidentResponse;

  constructor(
    private incidentService: IncidentService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
  
  public getIncidentTypeName(incidentType: IncidentType): string {
    switch (incidentType) {
      case IncidentType.OcupationalInjury:
        return 'Occupational Injury';
      case IncidentType.EnviromentSpill:
        return 'Environment Spill';
      case IncidentType.NearMiss:
        return 'Near Miss';
      default:
        return 'Unknown'; 
    }
  }

  public getSeverityTypeName(severityType: SeverityType): string {
    switch (severityType) {
      case SeverityType.Low:
        return 'Low';
      case SeverityType.Medium:
        return 'Medium';
      case SeverityType.High:
        return 'High';
      case SeverityType.Critical:
        return 'Critical';
      default:
        return 'Unknown';
    }
  }

  ngOnInit(): void {
    debugger
    this.idCurrentIncident = this.route.snapshot.paramMap.get('id')!;
    this.incidentService.getIncidentById(this.idCurrentIncident).subscribe((incident) => {
      this.incident = incident;
    });
  }
  
  public onDeleteIncident(): void {
    debugger
    this.incidentService.deleteIncident(this.idCurrentIncident).subscribe(() => {
      this.incidentService.showMessage('Incident deleted successfully');
      this.router.navigate(['dashboard/incident-read'])
    });

  }

}
