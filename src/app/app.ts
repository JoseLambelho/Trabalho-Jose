import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  protected title = 'recipe-site';
  userName: string = '';
  password: string = '';
  regName: string = '';
  regPass: string = '';
  regPassConfirm: string = '';
  isLoggedIn: boolean = false;
  view: 'welcome' | 'area' | 'create' | 'favorite' | 'comments' | 'created' = 'welcome';

  login() {
    if (this.userName && this.password) {
      this.isLoggedIn = true;
      this.view = 'welcome';
    } else {
      alert("Please fill in both fields.");
    }
  }

  register() {
    if (!this.regName || !this.regPass || !this.regPassConfirm) {
      alert("Fill in all fields.");
      return;
    }
    if (this.regPass !== this.regPassConfirm) {
      alert("Passwords do not match.");
      return;
    }
    this.userName = this.regName;
    this.isLoggedIn = true;
    this.view = 'welcome';
  }

  logout() {
    this.isLoggedIn = false;
    this.view = 'welcome';
    this.userName = '';
  }

  goTo(view: typeof this.view) {
    this.view = view;
  }
  
}
