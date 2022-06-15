import { Component, OnInit } from '@angular/core';

// Models
import { ModuleCard } from '@models/module-card.model';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.scss']
})
export class SecurityComponent implements OnInit {
  moduleCards: ModuleCard[] = [];

  constructor() { }

  ngOnInit(): void {
    this.moduleCards = [
      {
        title: 'Usuarios',
        subtitle: 'Opciones de usuario',
        avatar: 'person',
        content: 'Esto permite realizar operaciones de usuarios.',
        routerLink: '/security/users'
      },
      {
        title: 'Roles',
        subtitle: 'Opciones de usuario',
        avatar: 'groups',
        content: 'Esto permite realizar operaciones de usuarios.',
        routerLink: '/applications'
      },
      {
        title: 'Permissions',
        subtitle: 'Opciones de usuario',
        avatar: 'manage_accounts',
        content: 'Esto permite realizar operaciones de usuarios.',
        routerLink: '/applications'
      },
      {
        title: 'Others',
        subtitle: 'Opciones de usuario',
        avatar: 'home',
        content: 'Esto permite realizar operaciones de usuarios.',
        routerLink: '/applications'
      }
    ];
  }

}
