import { Routes } from '@angular/router';
import {NotFoundComponent} from "./component/not-found/not-found.component";
import {SignComponent} from "./component/sign/sign.component";
import {LogComponent} from "./component/log/log.component";
import {HomeComponent} from "./component/home/home.component";
import {UserComponent} from "./component/user/user.component";
import {UserAuthGuard} from "./guard/user-auth.guard";

export const routes: Routes = [
  {path : 'log', component: LogComponent},
  {path : 'profile', component: UserComponent, canActivate: [UserAuthGuard]},
  {path : 'sign', component: SignComponent},
  {path : '', component: HomeComponent},
  {path: '**', component: NotFoundComponent}
];
