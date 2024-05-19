import { Component } from '@angular/core';
import { IncidentService } from '../../services/incident.service';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import IncidentType from '../../shared/enums/incident-type.enum';
import SeverityType from '../../shared/enums/severity.enum';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {provideNativeDateAdapter} from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';


interface IncidentTypeSelect{
  value: IncidentType;
  viewValue: string;
}

interface SeveritySelect{
  value: SeverityType;
  viewValue: string;
}

@Component({
  selector: 'kuba-app-incident-create',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatCardModule, MatFormFieldModule, MatInputModule, 
    MatSelectModule, ReactiveFormsModule, CommonModule
    ,MatDatepickerModule,MatButtonModule
  ],
  templateUrl: './incident-create.component.html',
  styleUrl: './incident-create.component.css'
})
export class IncidentCreateComponent {

  public incidentCreateForm: FormGroup;
  public userId: string = localStorage.getItem('id')!;

  public incidentTypesSelect: IncidentTypeSelect[] =[
    {value: IncidentType.OcupationalInjury, viewValue: 'OcupationalInjury'},
    {value: IncidentType.EnviromentSpill, viewValue: 'EnviromentSpill'},
    {value: IncidentType.NearMiss, viewValue: 'NearMiss'}
  ];

  public severitiesSelect: SeveritySelect[] =[
    {value: SeverityType.Low, viewValue: 'Low'},
    {value: SeverityType.Medium, viewValue: 'Medium'},
    {value: SeverityType.High, viewValue: 'High'},
    {value: SeverityType.Critical, viewValue: 'Critical'}
  ]

  constructor(
    private incidentService: IncidentService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.incidentCreateForm = this.formBuilder.group({
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      severityType: [SeverityType.Low, [Validators.required]], 
      incidentType: [IncidentType.OcupationalInjury, [Validators.required]], 
      reportedDate: ['', [Validators.required]],
      userId: [this.userId, [Validators.required]]
    });
  }
    
  
  public onSubmitCreateIncident(): void {
    this.incidentService.createIncident(this.incidentCreateForm.value).subscribe(() => {
      this.incidentService.showMessage('Incident created successfully');
      this.router.navigate(['incident'])
    });

  }

  public cancel(): void {
    this.router.navigate(['dashboard/incident'])
  }

}
