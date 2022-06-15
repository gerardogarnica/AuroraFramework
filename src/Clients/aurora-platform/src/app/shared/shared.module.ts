import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// Components
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { ModuleCardsComponent } from './components/module-cards/module-cards.component';
import { ModuleCardDetailComponent } from './components/module-card-detail/module-card-detail.component';
import { ModuleTitleComponent } from './components/module-title/module-title.component';

// Modules
import { MaterialModule } from './../material/material.module';

@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    ModuleCardsComponent,
    ModuleCardDetailComponent,
    ModuleTitleComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    ModuleCardsComponent,
    ModuleCardDetailComponent,
    ModuleTitleComponent
  ]
})
export class SharedModule { }
