import { Component, Input, OnInit } from '@angular/core';

// Models
import { ModuleCard } from '@models/module-card.model';

@Component({
  selector: 'app-module-card-detail',
  templateUrl: './module-card-detail.component.html',
  styleUrls: ['./module-card-detail.component.scss']
})
export class ModuleCardDetailComponent implements OnInit {

  @Input('moduleCard') moduleCard!: ModuleCard;

  constructor() { }

  ngOnInit(): void {
  }

}
