import {Component, Input} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {UserService} from "../../../service/user.service";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-update',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
  ],
  templateUrl: './update.component.html',
  styleUrl: './update.component.scss'
})
export class UpdateComponent {
  form = new FormGroup({
    username: new FormControl(''),
    email: new FormControl(''),
  });

  constructor(private fb: FormBuilder, private userService: UserService) {}

  @Input() user: any;

  ngOnInit(): void {
    this.form = this.fb.group({
      username: [this.user?.username || '', [Validators.required, Validators.minLength(5)]],
      email: [this.user?.email || '', [Validators.required, Validators.minLength(5)]]
    });
  }

  onSubmit() {
    if (this.form.value.username && this.form.value.email) {
      this.userService.updateUser({
        Username: this.form.value.username,
        EmailAddress: this.form.value.email,
      }).subscribe(result => {
        localStorage.setItem('token', result.token)
      })
      this.form.reset();
    }
  }
}
