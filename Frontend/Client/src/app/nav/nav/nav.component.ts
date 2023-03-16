import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  currentUser$: Observable<User | null> = of(null)

  constructor(private auth: AuthService) {
  }

  ngOnInit(): void {
    this.currentUser$ = this.auth.currentUser$;
  }

  onLogin() {
    this.auth.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => console.log(error)
    });
  }
  onRegister() { }
  onLogout() {
    this.auth.logout();
    this.currentUser$ = of(null);
  }
}
