import { Component } from '@angular/core';
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  userConnect : boolean = false;

  constructor(private router: Router) {
  }

  ngOnInit(): void {
    const token = localStorage.getItem('token');

    if (token && token.length > 0) {
      this.userConnect = true;
    }
  }

  public logOut() {
    localStorage.clear();
    this.userConnect = false;
    this.router.navigateByUrl('/');
  }
}
