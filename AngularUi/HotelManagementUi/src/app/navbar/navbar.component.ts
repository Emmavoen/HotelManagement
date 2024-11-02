import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  @Input() sidebarActive: boolean = false;
  @Output() sidebarToggle = new EventEmitter<boolean>(); // Ensure the type is boolean

  toggleSidebar() {
    this.sidebarActive = !this.sidebarActive; // Toggle the state
    this.sidebarToggle.emit(this.sidebarActive); // Emit the boolean value
  }
}
