import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Routing
import { SecurityRoutingModule } from './security-routing.module';

// Components
import { SecurityComponent } from './pages/security/security.component';

// Modules
import { MaterialModule } from 'src/app/material/material.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    SecurityComponent
  ],
  imports: [
    CommonModule,
    SecurityRoutingModule,
    MaterialModule,
    SharedModule
  ]
})
export class SecurityModule { }
