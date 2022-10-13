import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '@app/models/user/Login';
import { User } from '@app/models/user/User';
import { UserService } from '@app/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public model = {} as Login;

  constructor(private userService: UserService,
    private router: Router,
    private toastService: ToastrService) { }

  ngOnInit() {

  }

  public login(): void {
    this.userService.login(this.model).subscribe({
      next: (user: User) => { this.router.navigateByUrl("/dashboard") },
      error: (error: any) => { this.toastService.error(error.error, "Failed") },
      complete: () => { }
    })
  }

}
