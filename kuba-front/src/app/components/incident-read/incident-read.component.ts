import { Component, OnInit } from '@angular/core';
import IncidentResponse from '../../shared/models/incident-response';
import { MatTableModule } from '@angular/material/table';
import { IncidentService } from '../../services/incident.service';
import IncidentType from '../../shared/enums/incident-type.enum';
import SeverityType from '../../shared/enums/severity.enum';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../services/account.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'kuba-app-incident-read',
  standalone: true,
  imports: [MatTableModule, CommonModule, RouterLink],
  templateUrl: './incident-read.component.html',
  styleUrl: './incident-read.component.css'
})
export class IncidentReadComponent implements OnInit {

  public incidents: IncidentResponse[] = [];
  public availableActions: string[] = [];
  public displayedColumns: string[] = ['title', 'severityType', 'incidentType', 'action'];
  public userRole: string = "";


  constructor(private incidentService: IncidentService, private accService: AccountService) { }


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

  public getActions(role: string): string[] {
    switch (role) {
      case 'Employee':
        return ['visibility'];
      case 'Supervisor':
        return ['edit'];
      case 'Adm':
        return ['edit', 'delete'];
      default:
        return []; 
    }
  }

  public getRouterLink(id: string, action: string): string {
    if (this.userRole === 'Employee') {
        return `/incident/${id}`;
    } else {
      if(action === 'delete') {
        return `/incident/delete/${id}`;
      }else {
        return `/incident/update/${id}`;
      }
    }
  }

  ngOnInit(): void {
   

    if(this.accService.getUserRole()! === 'Supervisor' || this.accService.getUserRole()! === 'Adm') {
      this.incidentService.getAllIncidents().subscribe((incidents) => {
        this.incidents = incidents;
      })
    } 
    
    else {
      this.incidentService.getIncidentsByUserId(localStorage.getItem('id')!).subscribe((incidents) => {
        this.incidents = incidents;
      })
    }
    const role: string = this.accService.getUserRole()!
    this.availableActions = this.getActions(role);
    this.userRole = role;

  }



}
