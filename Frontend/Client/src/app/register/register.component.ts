import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private auth: AuthService) { }

  ngOnInit(): void {

  }

  register() {
    this.auth.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: error => console.log(error)
    });
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
