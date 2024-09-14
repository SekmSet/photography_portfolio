import { Component } from '@angular/core';
import {UserService} from "../../service/user.service";
import {UpdateComponent} from "./update/update.component";
import {NgIf} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    UpdateComponent,
    NgIf,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent {

  user: any;
  editMode: boolean = false;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getUserInformation().subscribe(result => {
      this.user = {
        username: result.username,
        email: result.emailAddress,
        actif : result.actif
      }
    })
  }

  onUpdate() {
    this.editMode = true
  }

  onDelete() {
    this.userService.deleteUser().subscribe(result => {
      localStorage.removeItem("token")
    })
  }
}
