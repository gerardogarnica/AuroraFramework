import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Layouts
import { LayoutComponent } from './components/layout/layout.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
      },
      {
        path: 'home',
        loadChildren: () => import('@modules/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'applications',
        loadChildren: () => import('@modules/applications/applications.module').then(m => m.ApplicationsModule)
      },
      {
        path: 'security',
        loadChildren: () => import('@modules/security/security.module').then(m => m.SecurityModule)
      },
      {
        path: 'settings',
        loadChildren: () => import('@modules/settings/settings.module').then(m => m.SettingsModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
