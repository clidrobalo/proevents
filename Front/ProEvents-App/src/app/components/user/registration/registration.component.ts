import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/Validators';
import { User } from '@app/models/user/User';
import { UserService } from '@app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  private user: User = {} as User;
  registationForm!: FormGroup;

  get form(): any {
    return this.registationForm.controls;
  }

  constructor(private fb: FormBuilder,
    private userService: UserService,
    private toastService: ToastrService,
    private spinnerService: NgxSpinnerService,
    private router: Router) { }

  ngOnInit() {
    this.validation();
  }

  public register(): void {
    this.spinnerService.show();

    this.userService.register(this.registationForm.value).subscribe({
      next: (user: User) => {
        this.toastService.success(`User "${user.userName}" registered successful.`, "Success");
        this.router.navigateByUrl("/user/login");
      },
      error: (error) => {
        this.toastService.error(error.error, "Failed");
        this.spinnerService.hide();
      },
      complete: () => { this.spinnerService.hide(); },
    })
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'passwordConfirmed')
    };

    this.registationForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required, Validators.minLength(4)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      passwordConfirmed: ['', Validators.required]
    }, formOptions);
  }

  public resetForm(): void {
    this.registationForm.reset();
  }
}
