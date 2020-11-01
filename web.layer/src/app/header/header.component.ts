import { ChangeDetectionStrategy, Component, ViewEncapsulation,OnInit,OnDestroy} from '@angular/core';

@Component({
  selector: 'header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class HeaderComponent implements OnInit, OnDestroy {
 
  constructor() {

  }
  
  ngOnInit() {

  }
 
  ngOnDestroy() {

  }

}
