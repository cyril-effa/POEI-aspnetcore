# GestionBU SAS - Gestion des bibliothèques universitaires

![Schema](/Refactoring/Gestion_BU/Gestion_BU/wwwroot/img/schema.png)

GestionBU est une bibliothèque en ligne qui vend ses services aux universités. Les universités l'utilisent en mode SaaS (Software as a Service) pour permettre à leurs étudiants de lire et télécharger des livres électroniques.

Les utilisateurs de cette application (les étudiants) peuvent se connecter avec les informations d'identification fournies par leur université et télécharger des livres électroniques. Il y a une limite mensuelle d'e-books qu'un étudiant peut télécharger. Cette allocation mensuelle est basée sur le forfait acheté par l'université à laquelle appartient l'étudiant.

Pour qu'un étudiant puisse avoir accès aux livres grâce au forfait offert par son université, l'étudiant doit être inscrit dans le registre de GestionBU. Le registre relie un étudiant à une université.

Si le forfait souscrit par l'université de l'étudiant est :

- **Standard** : le nombre de téléchargements autorisé est de **10/mois**.
- **Premium** : le nombre de téléchargements autorisé est de **20/mois**.

### Aide Mémoire

- **S** : Single Responsibility Principle (SRP)  
  Une classe ne doit avoir qu'une seule et unique responsabilité.
- **O** : Open/Closed Principle (OCP)  
  Les entités doivent être ouvertes à l'extension et fermées à la modification.
- **L** : Liskov’s Substitution Principle (LSP)  
  Les objets dans un programme doivent être remplaçables par des instances de leur sous-type sans pour autant altérer le bon fonctionnement du programme.
- **I** : Interface Segregation Principle (ISP)  
  Aucun client ne devrait être forcé d'implémenter des méthodes/fonctions qu'il n'utilise pas.
- **D** : Dependency Inversion Principle (DIP)  
  Une classe doit dépendre de son abstraction, pas de son implémentation.

## Exercices

### Étape 1

1. En utilisant vos connaissances et les principes S.O.L.I.D, refactorisez l'application afin de permettre une meilleure séparation des couches et une meilleure testabilité/lisibilité.  
L'essentiel de vos changements s'effectueront au sein de la classe `RegistreService`.

### Étape 2

2. Imaginons maintenant que chaque forfait donne **la possibilité de souscrire un bonus** qui permet aux étudiants d'augmenter le nombre de téléchargements maximum :

    - **Standard** : Le bonus est de 5. (Total = 10 + 5)
    - **Premium** : Le bonus est de 10. (Total = 20 + 10)

    **Consigne** : Proposez une implémentation qui respecte ces nouvelles contraintes, tout en respectant au maximum les principes S.O.L.I.D.

### Étape 3

Il existe maintenant une nouvelle exigence pour ajouter un nouveau type de forfait, **"Illimité"**. Les étudiants des universités qui ont acheté ce forfait n'ont pas de limite d'allocation mensuelle.

**Consigne** : Proposez une implémentation qui respecte ces nouvelles contraintes, tout en respectant au maximum les principes S.O.L.I.D.
