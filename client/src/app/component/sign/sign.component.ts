import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {UserService} from "../../service/user.service";
import {User} from "../../interface/user";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-sign',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './sign.component.html',
  styleUrl: './sign.component.scss'
})
export class SignComponent {
  form = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(5)]),
    email: new FormControl('', [Validators.required, Validators.minLength(5)]),
    password: new FormControl('', [Validators.required, Validators.minLength(5)]),
  });

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    if (this.form.value.username && this.form.value.email && this.form.value.password) {
      this.userService.postUserCreate({
        Username: this.form.value.username,
        EmailAddress: this.form.value.email,
        Password: this.form.value.password
      }).subscribe(result => console.log(result))

      this.form.reset();
      this.router.navigateByUrl('/log');
    }
  }
}
