import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'kuba-app-incident',
  standalone: true,
  imports: [MatButtonModule, RouterModule],
  templateUrl: './incident.component.html',
  styleUrl: './incident.component.css'
})
export class IncidentComponent {
  constructor(private router: Router) { }

  public navigateToCreateIncident(): void {
    this.router.navigate(['/incident/create'])
  }
}
