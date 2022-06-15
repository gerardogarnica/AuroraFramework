import { Component, Input, OnInit } from '@angular/core';

// Models
import { ModuleCard } from '@models/module-card.model';

@Component({
  selector: 'app-module-cards',
  templateUrl: './module-cards.component.html',
  styleUrls: ['./module-cards.component.scss']
})
export class ModuleCardsComponent implements OnInit {

  @Input() moduleCards: ModuleCard[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
