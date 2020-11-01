import { ChangeDetectionStrategy, Component, ViewEncapsulation,OnInit,OnDestroy,Input} from '@angular/core';

@Component({
  selector: 'table-custom',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class TableComponent implements OnInit, OnDestroy {
  columns = [];
 @Input() columnData = [];

  constructor() { }
  
  ngOnInit() {
    if(this.columnData.length > 0) {
      this.columns = Object.keys(this.columnData[0]);
    }
  }
 
  ngOnDestroy() {}

}
