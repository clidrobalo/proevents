import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/Validators';
import { User } from '@app/models/user/User';
import { UserDetail } from '@app/models/user/UserDetail';
import { UserService } from '@app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
    public title = "Profile";
    profileForm!: FormGroup;

    private userDetail!: UserDetail;

    get form(): any {
        return this.profileForm.controls;
    }

    constructor(private fb: FormBuilder,
        private userService: UserService,
        private toastService: ToastrService,
        private spinnerService: NgxSpinnerService) { }

    ngOnInit() {
        this.validation();
        this.loadUserDetail();
    }

    private loadUserDetail(): void {
        this.spinnerService.show();

        this.userService.getUser().subscribe({
            next: (userDetail: UserDetail) => {
                this.userDetail = userDetail;
                this.profileForm.patchValue(this.userDetail);
            },
            error: (error) => {
                this.spinnerService.hide();
                this.toastService.error(error.message, "Failed")
            },
            complete: () => { this.spinnerService.hide(); }
        })
    }

    public updateUserDetail(): void {
        this.spinnerService.show();

        this.userService.update(this.profileForm.value).subscribe({
            next: (userDetail: UserDetail) => {
                this.userDetail = userDetail;
                this.toastService.success("User detail updated.", "Success")
            },
            error: (error) => {
                this.spinnerService.hide();
                this.toastService.error("Error in update user detail.", "Failed")
            },
            complete: () => { this.spinnerService.hide(); }
        })
    }

    public resetPassword() {
        this.spinnerService.show();

        this.userService.resetPassword(this.profileForm.value).subscribe({
            next: (userDetail: UserDetail) => {
                this.userDetail = userDetail;
                this.toastService.success("Password reseted.", "Success")
            },
            error: (error) => {
                this.spinnerService.hide();
                this.toastService.error(error.error, "Failed")
            },
            complete: () => { this.spinnerService.hide(); }
        })
    }

    private validation(): void {
        const formOptions: AbstractControlOptions = {
            validators: ValidatorField.MustMatch('password', 'passwordConfirmed')
        };

        this.profileForm = this.fb.group({
            id: [0],
            title: ['', [Validators.required]],
            userName: ['', [Validators.required, Validators.minLength(4)]],
            firstName: ['', [Validators.required, Validators.minLength(3)]],
            lastName: ['', [Validators.required, Validators.minLength(4)]],
            email: ['', [Validators.required, Validators.email]],
            phoneNumber: ['', [Validators.required]],
            function: ['', [Validators.required]],
            description: [''],
            password: ['', [Validators.required, Validators.minLength(6)]],
            passwordConfirmed: ['', Validators.required]
        }, formOptions);
    }

    public resetForm(event: any): void {
        event.preventDefault(); // to prevent reload
        this.profileForm.reset();
    }

}
