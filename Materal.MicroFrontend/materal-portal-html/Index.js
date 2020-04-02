import 'zone.js';
import './Index.less';
import { PortalApp } from './PortalApp/PortalApp';
import { subAppList } from './SubAppList'
const portalApp = new PortalApp();
portalApp.Init(subAppList);
