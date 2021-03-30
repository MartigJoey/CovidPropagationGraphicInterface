# LogBook
| Auteur | Date de début | Projet  | Description |
| ------------- |:-------------:| :-----|---|
| Joey Martig| 30.03.2021 | Interface graphique de "Covid propagation" | Réalisation de l'interface graphique du travail de diplôme "Covid propagation". L'interface graphique est composée d'éléments graphiques de visual studio et permet de mieux visualiser la situation de la simulation. |
# 30.03.2021
- Plan du projet
- Réflexion composition interface
- Première idée:
  -  Comportant un trajet simple qui sera la première à être implémenté. 
  -  Un autre trajet en bus qui consisterait en la personne allant vers le bus sans compter de période qui se déplacerait pendant la période puis il se déplace vers le sa destination sans compter de période.
  -  Et finalement un trajet comportant une personne qui dépose une autre personne à un endroit. Je vais encore réfléchir à celui-ci.
![UseCase 1](Medias/UseCase.png)
- Installation du concepteur de classe de visual studio
- Réalisation de la première structure initiale du projet
  - Les bâtiments contrairement à la simulation, ne sont différents que par leur couleur. Leur différence est donc simplement gérée par un enum et non par la création de plusieurs sous-classes.
  - La trajectoire peut être utile pour la visualisation, mais risque de disparaitre de la version finale.
  - Les véhicules ayant plus de différence entre eux (Taille, couleur, quantité de personnes, etc.) que les bâtiments, il y a donc deux sous-classes qui héritent d'un objet voiture. ![Diagramme de classe initial](Medias/ClassDiagram1.png)
- Documentation
  - Début de l'introduction.
    - Description du travail de stage.
    - Avantage pour le travail de diplome et utilité
  - Structure documentation
  - Réalisation du planning
  - Architecture du projet
    - Implémentation dans le travail de diplôme
    - Arborescence
  - Environnement
  - Cahier des charges
- Structure du projet
  - Lien avec la futur simulation
    - ~~2 en 1 ?~~
      - ~~Trop de fonctionnalité dans un seul objet~~
      - ~~Structure plus simple~~
    - Séparé ?
      - Code plus propre
      - Gourmant en ressources
      - Après génération de la simulation. Cloner le contenu ( individus, batiments ) en objets simplifiés et ne contenant que les données nécessaires.
      - Garder le même pointeur sur le planning qui contiendrait les destination de la personne.
    - ~~Séparé mais géré par la simulation ?~~ ou par un autre controlleur spécifique à la vue ?
  - ![Structure du projet](Medias/StructureProjet.png)
- Structure du code
  - Reflexion sur qui fait quoi ainsi que qui possède quoi
  - Impacte sur le travail de diplome (Implémentation)
  - ![Reflexion sur la structure du projet](Medias/Reflexion.png)
- Début du code de l'interface
  - Timer
  - Paint
# 31.03.2021