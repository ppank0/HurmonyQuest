import { GetAllJuries } from "../../features/contest/jury/GetAllJuries";
import {Profile} from '../../features/auth/components/Profile'
import { GetAllParticipants } from "../../features/contest/participant/GetAllParticipants";
import { AboutPage } from "../../pages/AboutPage";
import { HomePage2 } from "../../pages/HomePage2";
import { GetAllNominations } from "../../features/contest/nomination/GetAllNominations";
import { CreateNomination } from "../../features/contest/nomination/CreateNomination";

export const ROUTES = {
  HOME: "/",
  ABOUT: "/about",
  Jury: "/juries",
  Participant: "/participants",
  NOMINATIONS: "/nominations",
  CREATE_NOMINATION: "/nominations/create",

  DASHBOARD: "/dashboard",
  PROFILE: "/profile",
  ADMIN: "/admin",
};

export const publicRoutes = [
  { path: ROUTES.HOME, component: HomePage2},
  { path: ROUTES.Jury, component: GetAllJuries },
  { path: ROUTES.ABOUT, component: AboutPage },
  { path: ROUTES.Participant, component: GetAllParticipants},
  { path: ROUTES.NOMINATIONS, component: GetAllNominations},
  { path: ROUTES.CREATE_NOMINATION, component: CreateNomination },
];

export const privateRoutes = [
  { path: ROUTES.PROFILE, component: Profile },
];