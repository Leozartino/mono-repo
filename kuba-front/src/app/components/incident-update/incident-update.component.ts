import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import IncidentType from '../../shared/enums/incident-type.enum';
import SeverityType from '../../shared/enums/severity.enum';
import { IncidentService } from '../../services/incident.service';
import { ActivatedRoute, Router } from '@angular/router';
import IncidentResponse from '../../shared/models/incident-response';
import { provideNativeDateAdapter } from '@angular/material/core';


interface IncidentTypeSelect{
  value: IncidentType;
  viewValue: string;
}

interface SeveritySelect{
  value: SeverityType;
  viewValue: string;
}


@Component({
  selector: 'kuba-app-incident-update',
  standalone: true,
  providers: [
    provideNativeDateAdapter()
  ],
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, 
    MatSelectModule, ReactiveFormsModule, CommonModule
    ,MatDatepickerModule,MatButtonModule],
  templateUrl: './incident-update.component.html',
  styleUrl: './incident-update.component.css'
})
export class IncidentUpdateComponent implements OnInit {
  public incidentUpdateForm: FormGroup;
  public userId: string = localStorage.getItem('id')!;
  public idCurrentIncident: string = "";
  public incident: IncidentResponse = {} as IncidentResponse;

  public incidentTypesSelect: IncidentTypeSelect[] =[
    {value: IncidentType.OcupationalInjury, viewValue: 'OcupationalInjury'},
    {value: IncidentType.EnviromentSpill, viewValue: 'EnviromentSpill'},
    {value: IncidentType.NearMiss, viewValue: 'NearMiss'}
  ];

  public severitiesSelect: SeveritySelect[] = [
    {value: SeverityType.Low, viewValue: 'Low'},
    {value: SeverityType.Medium, viewValue: 'Medium'},
    {value: SeverityType.High, viewValue: 'High'},
    {value: SeverityType.Critical, viewValue: 'Critical'}
  ]

  constructor(
    private incidentService: IncidentService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.incidentUpdateForm = this.formBuilder.group({
      title: [''],
      description: [''],
      severityType: [null], 
      incidentType: [null], 
      reportedDate: [''],
      updatedBy: [this.userId]
    });
  }

  ngOnInit(): void {
    this.idCurrentIncident = this.route.snapshot.paramMap.get('id')!;
    this.incidentService.getIncidentById(this.idCurrentIncident).subscribe((incident) => {
      this.incident = incident;
      this.incidentUpdateForm = this.formBuilder.group({
        title: [this.incident.title],
        description: [this.incident.description],
        severityType: [this.incident.severityType], 
        incidentType: [this.incident.incidentType], 
        reportedDate: [this.incident.reportedDate],
        updatedBy: [this.userId]
      });
    });
  }


  public onSubmitUpdateIncident(): void {
    this.incidentService.updateIncident(this.idCurrentIncident, this.incidentUpdateForm.value).subscribe(() => {
      this.incidentService.showMessage('Incident updated successfully');
      this.router.navigate(['dashboard/incident-read'])
    });

  }

    
}
