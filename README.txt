Ce projet doit être compilé et exécuté avec Visual Studio 2013 (préférablement sur les ordinateurs de l'école).

Ouvrir le fichier ./GTI619_Lab5.sln avec Visual Studio.

Dans Visual Studio, accédez à l'explorateur de serveur (Server Explorer). S'il n'est pas accéssible, vous pouvez 
le trouver dans le menu [View > Server Explorer / Affichage > Explorateur de serveur].

Dans l'explorateur de serveur, sous "Data Connections" il y a un item "DefaultConnection (GTI619_Lab5)". Ouvrir
le menu contexte (click droit - right click) de cet item (donc right click sur DefaultConnection) puis sélectionnez
"New Query"/"Nouvelle Requête". Vous devriez voir une nouvelle fenêtre ouvrir.

Copier le contenu du fichier "./GTI619_Lab5/Scripts/SQL/1-DropErrthing.sql" dans la fenêtre que vous venez d'ouvrir,
puis exécutez le script à avec la commande Ctrl + Shift + E (ou le petit bouton "play" vert au dessus de la fenêtre).

Ouvrir le Package Manager Console de NuGet [Tools > NuGet Package Manager > Package Manager Console].

Dans la console, exécutez la commande "Update-Database". Il devrait y avoir aucune erreur.

De retour à la fenêtre d'exécution de requête dans la base de données; Effacer tout. Copiez le contenu des scripts
"./GTI619_Lab5/Scripts/SQL/2 - Roles + config.sql" dans la fenêtre puis exécutez le script à avec la commande Ctrl + Shift + E 
(ou le petit bouton "play" vert au dessus de la fenêtre).

Voilà, vous êtes maintenant près à démarrer le logiciel avec F5 ou le bouton dans le haut de l'IDE. Cette commande devrait
ouvrir une fenêtre dans votre furteur par défaut à l'adresse https://localhost:44300/

**************************
Les informations d'authentification de l'administrateur sont : 
u: Administrateur
p: Administrateur123!

**************************
Avec les configurations par défaut, l'authentification forte est désactivée. Si vous l'activez, vous ne receverez pas les
texto de validation car le service qu'on utilise nécessite qu'on enregistre le numéro de téléphone a priori. Si vous voulez,
vous pouvez m'envoyer votre # de téléphone (simoncorcos.ing@gmail.com) et je l'enregisterai aussi tôt.