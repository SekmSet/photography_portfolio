import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {UserService} from "../../service/user.service";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-log',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './log.component.html',
  styleUrl: './log.component.scss'
})
export class LogComponent {
  form = new FormGroup({
    usernameOrEmail: new FormControl('', [Validators.required, Validators.minLength(5)]),
    password: new FormControl('', [Validators.required, Validators.minLength(5)]),
  });

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    if (this.form.value.usernameOrEmail && this.form.value.password) {
      this.userService.postUserConnect({
        UsernameOrEmail: this.form.value.usernameOrEmail,
        Password: this.form.value.password
      }).subscribe(result => {
        localStorage.setItem('token', result.token)
        this.router.navigateByUrl('/profile');
      })
      this.form.reset();
    }
  }
}
