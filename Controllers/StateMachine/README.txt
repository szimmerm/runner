Dossier comprennant le système d'automates a etats finis
Pour l'utiliser, un module doit heriter AutomatonBuilder :
- on rajoute les etats dans la fonction BuildStates
- on rajoute les transitions dans la fonction BuildTransitions
- on donne les valeurs de base du contexte dans la fonction BuildContext
- on indique l'etat de depart avec la fonction SetBaseState

Un exemple est donne dans le module PlayerAutomaton
