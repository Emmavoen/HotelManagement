import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'HotelManagementUi';

  sidebarActive = false;

  handleSidebarToggle(event : boolean) {
    this.sidebarActive = event;
}
}
