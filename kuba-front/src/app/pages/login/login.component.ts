import { Component } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';
import { UserResponseLogin } from '../../shared/models/user-response-login';

@Component({
  selector: 'kuba-app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {

  public loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder, 
    private accountService: AccountService, 
    private router: Router
    ) {

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    })
    
  }

   get isEmailTouched() {
    return this.loginForm.get('email')?.touched ?? false ;
    
  }
  
  get isPasswordTouched() {
    return this.loginForm.get('password')?.touched ?? false ;
    
  }
  
  public showPassword: boolean  = false;

  public toggleEyePassword(): void {
    this.showPassword = !this.showPassword
  }

  public onInputChange(controlName: string) {
    this.loginForm.get(controlName)?.markAsTouched(); 
  }

  public onSubmit(): void {
    this.accountService.login(this.loginForm.value).subscribe((response: UserResponseLogin) => {
      localStorage.setItem('token', response.token);
      localStorage.setItem('id', response.id);
      localStorage.setItem('role', response.role);
      this.router.navigate(['dashboard']);
    });
  }
}
